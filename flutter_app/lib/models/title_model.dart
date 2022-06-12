// To parse this JSON data, do
//
//     final titles = titlesFromJson(jsonString);

import 'package:flutter_app/models/country_model.dart';
import 'package:flutter_app/models/genre_model.dart';
import 'dart:convert';

List<TitleModel> titlesFromJson(String str) =>
    List<TitleModel>.from(json.decode(str).map((x) => TitleModel.fromJson(x)));

String totlesToJson(List<TitleModel> data) =>
    json.encode(List<dynamic>.from(data.map((x) => x.toJson())));

class TitleModel {
  TitleModel({
    required this.title,
    required this.img,
    required this.titleType,
    required this.netflixId,
    required this.synopsis,
    required this.rating,
    required this.year,
    required this.runtime,
    required this.poster,
    required this.top250,
    required this.top250Tv,
    required this.titleDate,
    required this.countryModels,
    required this.genreModels,
    required this.titleImageModels,
  });

  String title;
  String img;
  String titleType;
  int netflixId;
  String synopsis;
  String rating;
  String year;
  String runtime;
  String poster;
  int top250;
  int top250Tv;
  DateTime titleDate;
  List<CountryModel> countryModels;
  List<GenreModel> genreModels;
  List<dynamic> titleImageModels;

  factory TitleModel.fromJson(Map<String, dynamic> json) => TitleModel(
        title: json["title"],
        img: json["img"],
        titleType: json["title_type"],
        netflixId: json["netflix_id"],
        synopsis: json["synopsis"],
        rating: json["rating"],
        year: json["year"],
        runtime: json["runtime"],
        poster: json["poster"],
        top250: json["top250"],
        top250Tv: json["top250tv"],
        titleDate: DateTime.parse(json["title_date"]),
        countryModels: List<CountryModel>.from(
            json["countryModels"].map((x) => CountryModel.fromJson(x))),
        genreModels: List<GenreModel>.from(
            json["genreModels"].map((x) => GenreModel.fromJson(x))),
        titleImageModels:
            List<dynamic>.from(json["titleImageModels"].map((x) => x)),
      );

  Map<String, dynamic> toJson() => {
        "title": title,
        "img": img,
        "title_type": titleType,
        "netflix_id": netflixId,
        "synopsis": synopsis,
        "rating": rating,
        "year": year,
        "runtime": runtime,
        "poster": poster,
        "top250": top250,
        "top250tv": top250Tv,
        "title_date":
            "${titleDate.year.toString().padLeft(4, '0')}-${titleDate.month.toString().padLeft(2, '0')}-${titleDate.day.toString().padLeft(2, '0')}",
        "countryModels":
            List<dynamic>.from(countryModels.map((x) => x.toJson())),
        "genreModels": List<dynamic>.from(genreModels.map((x) => x.toJson())),
        "titleImageModels": List<dynamic>.from(titleImageModels.map((x) => x)),
      };
}
