import 'package:flutter_app/models/genre_model.dart';

class WatcherGenreModel {
  WatcherGenreModel({
    required this.watcherName,
    required this.genreId,
    required this.watcherGenreScore,
    required this.genreModel,
  });

  String watcherName;
  int genreId;
  double watcherGenreScore;
  GenreModel genreModel;

  factory WatcherGenreModel.fromJson(Map<String, dynamic> json) =>
      WatcherGenreModel(
        watcherName: json["watcher_name"],
        genreId: json["genre_id"],
        watcherGenreScore: json["watcherGenreScore"],
        genreModel: GenreModel.fromJson(json["genreModel"]),
      );

  Map<String, dynamic> toJson() => {
        "watcher_name": watcherName,
        "genre_id": genreId,
        "watcherGenreScore": watcherGenreScore,
        "genreModel": genreModel.toJson(),
      };
}
