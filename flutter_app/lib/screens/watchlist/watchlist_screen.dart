import 'package:flutter/material.dart';
import 'package:flutter_app/components/menu_drawer.dart';
import 'package:flutter_app/constants.dart';
import 'package:flutter_app/models/title_model.dart';
import 'package:flutter_app/screens/watchlist/components/body.dart';
import 'dart:math';

import 'package:flutter_app/services/titles_service.dart';

class WatchlistScreen extends StatefulWidget {
  const WatchlistScreen({Key? key}) : super(key: key);

  @override
  _WatchlistScreenState createState() => _WatchlistScreenState();
}

class _WatchlistScreenState extends State<WatchlistScreen> {
  //final _scaffoldKey = new GlobalKey<ScaffoldState>();
  final ScrollController _controller = ScrollController();

  static List<Body> movies = [];

  bool _isLoading = false;
  // ignore: prefer_final_fields
  static List<Body> _dummy = List.generate(min(movies.length, kMoviesToLoad),
      (index) => movies[index]); //movies shown on screen
  static List<Body> toRemove = []; //movies to remove from _dummy on next reload
  static List<Body> removedMovies = []; //movies already removed from _dummy

  @override
  void initState() {
    loadTitles();
    _controller.addListener(_onScroll);
    super.initState();
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  _onScroll() {
    if (_controller.offset >=
            _controller.position.maxScrollExtent -
                kPixelsBeforeRenderingNewMovies &&
        !_isLoading &&
        _dummy.length - toRemove.length + removedMovies.length <
            movies.length) {
      setState(() {
        _isLoading = true;
      });
      _fetchData();
    }
  }

  Future _fetchData() async {
    setState(() {
      _dummy.removeWhere((element) => toRemove.contains(element));
      removedMovies.addAll(toRemove);
      toRemove = [];
      int lastIndex = _dummy.length + removedMovies.length;
      _dummy.addAll(List.generate(
          min(movies.length - (_dummy.length + removedMovies.length),
              kMoviesToLoad),
          (index) => movies[lastIndex + index]));
    });
    //TODO:moving await future before setState removes the need for scrolling after loading butcreates listview problems
    await Future.delayed(const Duration(seconds: 2));
    _isLoading = false;
  }

  void loadTitles() {
    print("Loading titles in watchlist...");
    TitlesService.getTitles(filterFlags: kInWatchLater).then((res) => {
          if (res.statusCode == 200)
            {
              for (var titleModel in titlesFromJson(res.body))
                {
                  TitlesService.getPreferenceModel(titleModel)
                      .then((watcherTitlePreferenceModel) => {
                            if (watcherTitlePreferenceModel != null)
                              {
                                movies.add(Body(
                                    movie: watcherTitlePreferenceModel.clone()))
                              }
                          })
                }
            }
        });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      drawerEnableOpenDragGesture: false,
      drawerEdgeDragWidth: 0,
      appBar: buildAppBar(),
      endDrawer: const MenuDrawer(),
      body: ListView.builder(
        controller: _controller,
        itemCount: _isLoading ? _dummy.length + 1 : _dummy.length,
        itemBuilder: (context, index) {
          if (_dummy.length - toRemove.length == index &&
              _dummy.length + removedMovies.length < movies.length) {
            return const Center(child: CircularProgressIndicator());
          }

          if (_dummy.length <= index) {
            return const SizedBox.shrink();
          }

          return Dismissible(
              key: UniqueKey(),
              background: swipeActionRight(),
              child: _dummy[index],
              direction: DismissDirection.endToStart,
              onDismissed: (direction) {
                toRemove.add(_dummy[index]);
                if ((_dummy.length - toRemove.length < kmaxMoviesOnScreen &&
                    _dummy.length + removedMovies.length < movies.length)) {
                  setState(() {
                    _isLoading = true;
                  });
                  _fetchData();
                }
              });
        },
      ),
    );
  }
}

swipeActionRight() => Container(
    alignment: Alignment.centerRight,
    //padding: EdgeInsets.symmetric(horizontal: 20),
    color: kIconColor,
    child: const Icon(
      Icons.delete_forever,
      color: kPrimaryColor,
      size: 75,
    ));

AppBar buildAppBar() {
  return AppBar(
    backgroundColor: kAppBarColor,
    shadowColor: Colors.black,
    iconTheme: const IconThemeData(
      color: kPrimaryColor,
      size: 40,
    ),
  );
}
