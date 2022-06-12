class CountryModel {
  CountryModel({
    required this.id,
    required this.country,
    required this.countrycode,
    required this.expiring,
    required this.nl7,
    required this.tmovs,
    required this.tseries,
    required this.tvids,
  });

  int id;
  String country;
  String countrycode;
  int expiring;
  int nl7;
  int tmovs;
  int tseries;
  int tvids;

  factory CountryModel.fromJson(Map<String, dynamic> json) => CountryModel(
        id: json["id"],
        country: json["country"],
        countrycode: json["countrycode"],
        expiring: json["expiring"],
        nl7: json["nl7"],
        tmovs: json["tmovs"],
        tseries: json["tseries"],
        tvids: json["tvids"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "country": country,
        "countrycode": countrycode,
        "expiring": expiring,
        "nl7": nl7,
        "tmovs": tmovs,
        "tseries": tseries,
        "tvids": tvids,
      };
}
