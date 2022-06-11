class TitleModel {
  final String watcherName;
  final int netflixId;
  final int preference;
  final DateTime prefLastSetTime;
  final bool watchLater;
  final DateTime watchLaterLastSetTime;

  const WatcherTitlePreferenceModel(
      {required this.watcherName,
      required this.netflixId,
      required this.preference,
      required this.prefLastSetTime,
      required this.watchLater,
      required this.watchLaterLastSetTime});

  factory WatcherTitlePreferenceModel.fromJson(Map<String, dynamic> json) {
    return WatcherTitlePreferenceModel(
        watcherName: json['watcher_name'],
        netflixId: json['netflix_id'],
        preference: json['preference'],
        prefLastSetTime: json['prefLastSetTime'],
        watchLater: json['watchLater'],
        watchLaterLastSetTime: json['watchLaterLastSetTime']);
  }

  @override
  String toString() {
    return {
      "watcher_name": watcherName,
      "netflix_id": netflixId,
      "preference": preference,
      "prefLastSetTime": prefLastSetTime,
      "watchLater": watchLater,
      "watchLaterLastSetTime": watchLaterLastSetTime
    }.toString();
  }

  Map<String, dynamic> toJson() => {
        "watcher_name": watcherName,
        "netflix_id": netflixId,
        "preference": preference,
        "prefLastSetTime": prefLastSetTime,
        "watchLater": watchLater,
        "watchLaterLastSetTime": watchLaterLastSetTime
      };
}
