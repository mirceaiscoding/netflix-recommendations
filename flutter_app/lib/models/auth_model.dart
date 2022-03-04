class AuthModel {
  final String accessToken;

  const AuthModel({
    required this.accessToken,
  });

  factory AuthModel.fromJson(Map<String, dynamic> json) {
    return AuthModel(
      accessToken: json['accessToken'],
    );
  }
}
