import 'package:flutter/foundation.dart';
import '../services/websocket_service.dart';

class ScheduleProvider with ChangeNotifier {
  final WebSocketService _webSocketService = WebSocketService();

  ScheduleProvider() {
    _initializeWebSocket();
  }

  Future<void> _initializeWebSocket() async {
    await _webSocketService.initializeConnection();
  }

  Future<void> updateSchedule() async {
    await _webSocketService.requestScheduleUpdate();
  }

  @override
  void dispose() {
    _webSocketService.dispose();
    super.dispose();
  }
}