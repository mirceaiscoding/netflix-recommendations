// To parse this JSON data, do
//
//     final genreUpdateModel = genreUpdateModelFromJson(jsonString);

import 'dart:convert';

List<GenreUpdateModel> genreUpdateModelFromJson(String str) =>
    List<GenreUpdateModel>.from(
        json.decode(str).map((x) => GenreUpdateModel.fromJson(x)));

String genreUpdateModelToJson(List<GenreUpdateModel> data) =>
    json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class GenreUpdateModel {
  GenreUpdateModel({
    required this.genreId,
    required this.watcherGenreScore,
  });

  int genreId;
  double watcherGenreScore;

  factory GenreUpdateModel.fromJson(Map<String, dynamic> json) =>
      GenreUpdateModel(
        genreId: json["genre_id"],
        watcherGenreScore: json["watcherGenreScore"],
      );

  Map<String, dynamic> toJson() => {
        "genre_id": genreId,
        "watcherGenreScore": watcherGenreScore,
      };

  @override
  String toString() {
    return toJson().toString();
  }
}
