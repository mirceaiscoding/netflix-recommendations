import 'package:flutter/material.dart';
import 'package:flutter_app/components/menu_drawer.dart';
import 'package:flutter_app/constants.dart';
import 'package:flutter_app/screens/watchlist/components/body.dart';
import 'dart:math';

class WatchlistScreen extends StatefulWidget {
  const WatchlistScreen({Key? key}) : super(key: key);

  @override
  _WatchlistScreenState createState() => _WatchlistScreenState();
}

class _WatchlistScreenState extends State<WatchlistScreen> {

  //final _scaffoldKey = new GlobalKey<ScaffoldState>();
  final ScrollController _controller = ScrollController();

  static const List<Body>movies = [
                                    Body(movie: {
                                      'title': 'Bumblebee',
                                      'year': '2018',
                                      'score': '6.7',
                                      'number_of_reviews': '156k',
                                      'description':
                                          "On the run in the year 1987, Bumblebee finds refuge in a junkyard in a small California beach town. On the cusp of turning 18 and trying to find her place in the world, Charlie Watson discovers Bumblebee, battle-scarred and broken.",
                                      'poster':
                                          "https://m.media-amazon.com/images/M/MV5BMjUwNjU5NDMyNF5BMl5BanBnXkFtZTgwNzgxNjM2NzM@._V1_.jpg",
                                    }),
                                    Body(movie: {
                                      'title': 'Cars 3',
                                      'year': '2017',
                                      'score': '6.7',
                                      'number_of_reviews': '91k',
                                      'description':
                                          "Lightning McQueen sets out to prove to a new generation of racers that he's still the best race car in the world.",
                                      'poster':
                                          "https://cdn.europosters.eu/image/750/cars-3-duel-i97645.jpg",
                                    }),
                                    Body(movie: {
                                      'title': 'Megamind',
                                      'year': '2010',
                                      'score': '7.3',
                                      'number_of_reviews': '249k',
                                      'description':
                                          "Evil genius Megamind finally defeats his do-gooder nemesis, Metro Man, but is left without a purpose in a superhero-free world.",
                                      'poster':
                                          "https://imgc.allpostersimages.com/img/posters/megamind_u-L-F3WOKT0.jpg?artHeight=550&artPerspective=n&artWidth=550"
                                    }),
                                    Body(movie: {
                                      'title': 'Shrek',
                                      'year': '2001',
                                      'score': '7.9',
                                      'number_of_reviews': '660k',
                                      'description':
                                          "A mean lord exiles fairytale creatures to the swamp of a grumpy ogre, who must go on a quest and rescue a princess for the lord in order to get his land back.",
                                      'poster':
                                          "https://m.media-amazon.com/images/M/MV5BOGZhM2FhNTItODAzNi00YjA0LWEyN2UtNjJlYWQzYzU1MDg5L2ltYWdlL2ltYWdlXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_.jpg",
                                    }),
                                    Body(movie: {
                                      'title': 'Bee Movie',
                                      'year': '2007',
                                      'score': '6.1',
                                      'number_of_reviews': '158k',
                                      'description':
                                          "Barry B. Benson, a bee just graduated from college, is disillusioned at his lone career choice: making honey. On a special trip outside the hive, Barry's life is saved by Vanessa, a florist in New York City. As their relationship blossoms, he discovers humans actually eat honey, and subsequently decides to sue them.",
                                      'poster':
                                          "https://m.media-amazon.com/images/M/MV5BMjE1MDYxOTA4MF5BMl5BanBnXkFtZTcwMDE0MDUzMw@@._V1_.jpg",
                                    }),
                                    Body(movie: {
                                      'title': 'The Road to El Dorado',
                                      'year': '2000',
                                      'score': '6.9',
                                      'number_of_reviews': '94k',
                                      'description':
                                          "Two swindlers get their hands on a map to the fabled city of gold, El Dorado.",
                                      'poster':
                                          "https://m.media-amazon.com/images/M/MV5BOTEzNWIwMzctOTE1YS00YjIyLTgwZGEtMTMxZDAzNzlmODMxXkEyXkFqcGdeQXVyMjgyMDk1MzY@._V1_FMjpg_UX1000_.jpg",
                                    }),
                                  ];
  bool _isLoading = false;
  List<Body> _dummy = List.generate(min(movies.length, kMoviesToLoad), (index) => movies[index]);

  @override
  void initState() {
    _controller.addListener(_onScroll);
    super.initState();
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  _onScroll() {
    if (
      _controller.offset >= _controller.position.maxScrollExtent - kPixelsBeforeRenderingNewMovies
      && !_isLoading && _dummy.length<movies.length
      ) {
      setState(() {
        _isLoading = true;
      });
      _fetchData();
    }
  }

  Future _fetchData() async {
    await new Future.delayed(new Duration(seconds: 2));
    int lastIndex = _dummy.length;

    setState(() {
      _dummy.addAll(List.generate(
          min(movies.length - _dummy.length, kMoviesToLoad), (index) => movies[lastIndex+index]));
      _isLoading = false;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      drawerEnableOpenDragGesture: false,
      drawerEdgeDragWidth: 0,
      //key: _scaffoldKey,
      appBar: buildAppBar(),
      endDrawer: const MenuDrawer(),

      body: ListView.builder(
        controller: _controller,
        itemCount: _isLoading ? _dummy.length + 1 : _dummy.length,
        itemBuilder: (context, index) {
          if (kDebugMode) {
            print("Dummy length ${_dummy.length}");
            print("Index ${index}");
          }

          if (_dummy.length == index && _dummy.length<movies.length)
            return Center(child: CircularProgressIndicator());
          
          return _dummy[index];
        },
      ),
    );
  }
}

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
