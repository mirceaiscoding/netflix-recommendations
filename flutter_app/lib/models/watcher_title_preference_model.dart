// To parse this JSON data, do
//
//     final watcherTitlePreferenceModel = watcherTitlePreferenceModelFromJson(jsonString);

import 'package:flutter_app/models/country_model.dart';
import 'dart:convert';

import 'package:flutter_app/models/watcher_genre_model.dart';

List<WatcherTitlePreferenceModel> watcherTitlePreferenceModelsFromJson(
        String str) =>
    List<WatcherTitlePreferenceModel>.from(
        json.decode(str).map((x) => WatcherTitlePreferenceModel.fromJson(x)));

WatcherTitlePreferenceModel watcherTitlePreferenceModelFromJson(String str) =>
    WatcherTitlePreferenceModel.fromJson(json.decode(str));

String watcherTitlePreferenceModelToJson(WatcherTitlePreferenceModel data) =>
    json.encode(data.toJson());

class WatcherTitlePreferenceModel {
  WatcherTitlePreferenceModel({
    required this.watcherName,
    required this.netflixId,
    required this.title,
    required this.preference,
    required this.prefLastSetTime,
    required this.watchLater,
    required this.watchLaterLastSetTime,
    required this.synopsis,
    required this.rating,
    required this.year,
    required this.poster,
    required this.countryModels,
    required this.watcherGenreModels,
  });

  String watcherName;
  int netflixId;
  String title;
  int preference; // 0 if none | 1 if liked | 2 if disliked
  DateTime prefLastSetTime;
  bool watchLater; // true if in watchlist
  DateTime watchLaterLastSetTime;
  String synopsis;
  String rating;
  String year;
  String poster;
  List<CountryModel> countryModels;
  List<WatcherGenreModel> watcherGenreModels;

  factory WatcherTitlePreferenceModel.fromJson(Map<String, dynamic> json) =>
      WatcherTitlePreferenceModel(
        watcherName: json["watcher_name"],
        netflixId: json["netflix_id"],
        title: json["title"],
        preference: json["preference"],
        prefLastSetTime: DateTime.parse(json["prefLastSetTime"]),
        watchLater: json["watchLater"],
        watchLaterLastSetTime: DateTime.parse(json["watchLaterLastSetTime"]),
        synopsis: json["synopsis"],
        rating: json["rating"],
        year: json["year"],
        poster: json["poster"],
        countryModels: List<CountryModel>.from(
            json["countryModels"].map((x) => CountryModel.fromJson(x))),
        watcherGenreModels: List<WatcherGenreModel>.from(
            json["watcherGenreModels"]
                .map((x) => WatcherGenreModel.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "watcher_name": watcherName,
        "netflix_id": netflixId,
        "title": title,
        "preference": preference,
        "prefLastSetTime": prefLastSetTime.toIso8601String(),
        "watchLater": watchLater,
        "watchLaterLastSetTime": watchLaterLastSetTime.toIso8601String(),
        "synopsis": synopsis,
        "rating": rating,
        "year": year,
        "poster": poster,
        "countryModels":
            List<dynamic>.from(countryModels.map((x) => x.toJson())),
        "watcherGenreModels":
            List<dynamic>.from(watcherGenreModels.map((x) => x.toJson())),
      };

  @override
  String toString() {
    return "${preference}, ${watchLater}";
  }

  WatcherTitlePreferenceModel clone() => WatcherTitlePreferenceModel(
      watcherName: watcherName,
      netflixId: netflixId,
      title: title,
      preference: preference,
      prefLastSetTime: prefLastSetTime,
      watchLater: watchLater,
      watchLaterLastSetTime: watchLaterLastSetTime,
      synopsis: synopsis,
      rating: rating,
      year: year,
      poster: poster,
      countryModels: countryModels,
      watcherGenreModels: watcherGenreModels);
}
