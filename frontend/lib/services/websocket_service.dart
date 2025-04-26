import 'package:signalr_core/signalr_core.dart';

class WebSocketService {
  late HubConnection hubConnection;
  final String _hubUrl = 'http://localhost:5000/schedulehub';

  Future<void> initializeConnection() async {
    hubConnection = HubConnectionBuilder()
        .withUrl(_hubUrl, HttpConnectionOptions(
          logging: (level, message) => print(message),
        ))
        .withAutomaticReconnect()
        .build();

    hubConnection.on('ReceiveScheduleUpdate', (message) {
      print('Received schedule update: $message');
      // Handle the schedule update
    });

    try {
      await hubConnection.start();
      print('Connected to WebSocket');
    } catch (e) {
      print('Error connecting to WebSocket: $e');
    }
  }

  Future<void> requestScheduleUpdate() async {
    try {
      await hubConnection.invoke('SendScheduleUpdate');
    } catch (e) {
      print('Error sending schedule update: $e');
    }
  }

  void dispose() {
    hubConnection.stop();
  }
}