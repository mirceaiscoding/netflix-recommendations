import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter_app/constants.dart';
import 'package:flutter_app/screens/home/home_screen.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:integration_test/integration_test.dart';

void main() {
  IntegrationTestWidgetsFlutterBinding.ensureInitialized();

  group('swipe movie tests', () {
    testWidgets('press next button', (WidgetTester tester) async {
      await tester.pumpWidget(MaterialApp(
        debugShowCheckedModeBanner: false, // Remove 'debug' label from app
        title: 'Netflix Recommendations',
        theme: ThemeData(
          scaffoldBackgroundColor: kBackgroundColor,
          primaryColor: kPrimaryColor,
          // textTheme: Theme.of(context).textTheme.apply(bodyColor: kTextColor),
          visualDensity: VisualDensity.adaptivePlatformDensity,
        ),

        initialRoute: '/home', // Route when the app opens
        routes: {
          '/home': (context) => const Homescreen(),
        },
      ));
      await tester.pumpAndSettle();
      sleep(const Duration(seconds: 2));

      // State of homescreen
      final HomescreenState homescreenState =
          tester.state(find.byType(Homescreen));

      // Check if current page index is 0
      expect(0, homescreenState.currentPage);
      if (kDebugMode) {
        print("Page index: ${homescreenState.currentPage}");
      }

      // Search for the menu button
      var nextButton = find.byIcon(Icons.arrow_forward_ios_outlined);
      await tester.pumpAndSettle();
      expect(nextButton, findsOneWidget);

      // Emulate a tap on the next button
      await tester.tap(nextButton);
      await tester.pumpAndSettle();
      sleep(const Duration(seconds: 2));

      expect(1, homescreenState.currentPage);
      if (kDebugMode) {
        print("Page index: ${homescreenState.currentPage}");
      }

      // Emulate a tap on the next button
      await tester.tap(nextButton);
      await tester.pumpAndSettle();
      sleep(const Duration(seconds: 2));

      expect(2, homescreenState.currentPage);
      if (kDebugMode) {
        print("Page index: ${homescreenState.currentPage}");
      }
    });
  });
}
