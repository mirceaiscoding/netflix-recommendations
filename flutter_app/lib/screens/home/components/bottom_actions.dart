import 'package:flutter/material.dart';
import 'package:flutter_app/constants.dart';

class BottomActions extends StatelessWidget {
  const BottomActions({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      color: kPrimaryColor,
      height: 80.0,
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceEvenly,
        children: <Widget>[
          IconButton(
            onPressed: nextMovie(),
            iconSize: 40,
            icon: const Icon(
              Icons.arrow_back_ios_outlined,
              color: kIconColor,
            ),
          ),
          IconButton(
            onPressed: dislikeMovie(),
            iconSize: 40,
            icon: const Icon(
              Icons.sentiment_dissatisfied_outlined,
              color: kIconColor,
            ),
          ),
          IconButton(
            onPressed: addToWatchlist(),
            iconSize: 50,
            icon: const Icon(
              Icons.add_circle,
              color: kIconColor,
            ),
          ),
          IconButton(
            onPressed: likeMovie(),
            iconSize: 40,
            icon: const Icon(
              Icons.sentiment_satisfied_outlined,
              color: kIconColor,
            ),
          ),
          IconButton(
            onPressed: nextMovie(),
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

  dislikeMovie() {
    // TODO
  }

  likeMovie() {
    // TODO
  }

  nextMovie() {
    // TODO
  }

  previousMovie() {
    // TODO
  }

  addToWatchlist() {
    // TODO
  }
}
