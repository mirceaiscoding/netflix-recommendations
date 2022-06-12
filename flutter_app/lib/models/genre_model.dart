class GenreModel {
  GenreModel({
    required this.genre,
    required this.genreId,
  });

  String genre;
  int genreId;

  factory GenreModel.fromJson(Map<String, dynamic> json) => GenreModel(
        genre: json["genre"],
        genreId: json["genre_id"],
      );

  Map<String, dynamic> toJson() => {
        "genre": genre,
        "genre_id": genreId,
      };
}
