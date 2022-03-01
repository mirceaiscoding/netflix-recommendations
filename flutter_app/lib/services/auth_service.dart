import 'dart:convert';

import 'package:flutter_app/constants.dart';
import 'package:http/http.dart' as http;

class AuthService {
  Future<http.Response> register(String email, String password) {
    return http.post(
      Uri.parse(kAuthRequestURL + "Signup"),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(<String, String>{
        'email': email,
        'password': password,
      }),
    );
  }

  Future<http.Response> login(String email, String password) {
    return http.post(
      Uri.parse(kAuthRequestURL + "Login"),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(<String, String>{
        'email': email,
        'password': password,
      }),
    );
  }
}
