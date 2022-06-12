import 'package:flutter/material.dart';
import 'package:flutter_app/constants.dart';
import 'package:flutter_app/models/watcher_title_preference_model.dart';

class BottomActions extends StatefulWidget {
  final Function(int) onPageChanged;

  final WatcherTitlePreferenceModel title;

  const BottomActions(Key? key, this.onPageChanged, this.title)
      : super(key: key);

  @override
  State<BottomActions> createState() => _BottomActions2State();
}

class _BottomActions2State extends State<BottomActions> {
  // ACTIONS
  dislikeMovie() {
    setState(() {
      if (kDebugMode) {
        print("pressed dislike!");
      }
      if (widget.title.preference != 2) {
        widget.title.preference = 2;
      } else {
        widget.title.preference = 0;
      }
    });
  }

  likeMovie() {
    setState(() {
      if (kDebugMode) {
        print("pressed like!");
      }
      if (widget.title.preference != 1) {
        widget.title.preference = 1;
      } else {
        widget.title.preference = 0;
      }
    });
  }

  addToWatchlist() {
    setState(() {
      if (kDebugMode) {
        print("pressed add to watchlist!");
      }
      if (widget.title.watchLater == false) {
        widget.title.watchLater = true;
      } else {
        widget.title.watchLater = false;
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      color: kPrimaryColor,
      height: 80.0,
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceEvenly,
        children: <Widget>[
          IconButton(
            onPressed: () {
              widget.onPageChanged(-1);
            },
            iconSize: 40,
            icon: const Icon(
              Icons.arrow_back_ios_outlined,
              color: kIconColor,
            ),
          ),
          IconButton(
            onPressed: () {
              dislikeMovie();
            },
            iconSize: 40,
            icon: (widget.title.preference == 2
                ? const Icon(
                    Icons.sentiment_very_dissatisfied_outlined,
                    color: kIconColor,
                  )
                : const Icon(
                    Icons.sentiment_dissatisfied_outlined,
                    color: kIconNotSelectedColor,
                  )),
          ),
          IconButton(
            onPressed: () {
              addToWatchlist();
            },
            iconSize: 50,
            icon: (widget.title.watchLater
                ? const Icon(
                    Icons.check_circle,
                    color: kIconColor,
                  )
                : const Icon(
                    Icons.add_circle_outline,
                    color: kIconNotSelectedColor,
                  )),
          ),
          IconButton(
            onPressed: () {
              likeMovie();
            },
            iconSize: 40,
            icon: (widget.title.preference == 1
                ? const Icon(
                    Icons.sentiment_very_satisfied_outlined,
                    color: kIconColor,
                  )
                : const Icon(
                    Icons.sentiment_satisfied_outlined,
                    color: kIconNotSelectedColor,
                  )),
          ),
          IconButton(
            onPressed: () {
              widget.onPageChanged(1);
            },
            iconSize: 40,
            icon: const Icon(
              Icons.arrow_forward_ios_outlined,
              color: kIconColor,
            ),
          ),
        ],
      ),
    );
  }
}
