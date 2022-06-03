import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter_app/constants.dart';
import 'package:flutter_app/screens/home/home_screen.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:integration_test/integration_test.dart';

void main() {
  IntegrationTestWidgetsFlutterBinding.ensureInitialized();

  group('menu drawer tests', () {
    testWidgets('open menu', (WidgetTester tester) async {
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

      // Search for the menu button
      var menuButton = find.byIcon(Icons.menu);
      await tester.pumpAndSettle();
      expect(menuButton, findsOneWidget);
      expect(find.text('ACCOUNT'), findsNothing);

      // Emulate a tap on the menu button
      await tester.tap(menuButton);
      await tester.pumpAndSettle();
      sleep(const Duration(seconds: 2));

      // Verify the menu is open
      expect(find.text('ACCOUNT'), findsOneWidget);
    });
  });
}
