import 'dart:convert';
import 'package:web_socket_channel/web_socket_channel.dart';

class WebSocketService {
  final WebSocketChannel channel;

  WebSocketService(String url) : channel = WebSocketChannel.connect(Uri.parse(url));

  void sendMessage(String message) {
    channel.sink.add(message);
  }

  Stream<dynamic> get messages => channel.stream.map((event) => jsonDecode(event));

  void dispose() {
    channel.sink.close();
  }
}
