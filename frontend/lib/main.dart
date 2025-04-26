import 'package:flutter/material.dart';
import 'package:signalr_core/signalr_core.dart';
import 'config.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return const MaterialApp(
      home: ChatPage(),
    );
  }
}

class ChatPage extends StatefulWidget {
  const ChatPage({super.key});

  @override
  State<ChatPage> createState() => _ChatPageState();
}

class _ChatPageState extends State<ChatPage> {
  final TextEditingController _messageController = TextEditingController();
  late HubConnection hubConnection;
  final List<String> messages = [];

  @override
  void initState() {
    super.initState();
    initSignalR();
  }

  void initSignalR() async {
    hubConnection = HubConnectionBuilder()
        .withUrl('${Config.baseUrl}/schedulehub',
            HttpConnectionOptions(
              logging: (level, message) => print(message),
              skipNegotiation: true, 
              transport: HttpTransportType.webSockets,  
            ))
        .withAutomaticReconnect() 
        .build();

    hubConnection.onclose((error) {
      print("Connection closed: ${error?.toString()}");
    });

    hubConnection.on('ReceiveMessage', (message) {
      setState(() {
        messages.add(message![0].toString());
      });
    });

    try {
      await hubConnection.start();
      print("Connected to SignalR!");
    } catch (e) {
      print("Error connecting to SignalR: ${e.toString()}");
    }
  }

  Future<void> sendMessage() async {
    if (hubConnection.state == HubConnectionState.connected) {
      await hubConnection.invoke('SendMessage', args: [_messageController.text]);
      _messageController.clear();
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('WebSocket Test'),
      ),
      body: Column(
        children: [
          Expanded(
            child: ListView.builder(
              itemCount: messages.length,
              itemBuilder: (context, index) {
                return ListTile(
                  title: Text(messages[index]),
                );
              },
            ),
          ),
          Padding(
            padding: const EdgeInsets.all(8.0),
            child: Row(
              children: [
                Expanded(
                  child: TextField(
                    controller: _messageController,
                    decoration: const InputDecoration(
                      hintText: 'Enter message',
                    ),
                  ),
                ),
                IconButton(
                  icon: const Icon(Icons.send),
                  onPressed: sendMessage,
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }

  @override
  void dispose() {
    hubConnection.stop();
    _messageController.dispose();
    super.dispose();
  }
}
