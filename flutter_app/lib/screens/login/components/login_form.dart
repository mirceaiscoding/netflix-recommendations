import 'package:flutter/material.dart';
import 'package:flutter_app/constants.dart';
import 'package:flutter_app/screens/home/home_screen.dart';
import 'package:flutter_app/screens/register/register_screen.dart';

class LoginForm extends StatefulWidget {
  const LoginForm({Key? key}) : super(key: key);

  @override
  _LoginFormState createState() => _LoginFormState();
}

class _LoginFormState extends State<LoginForm> {
  // global key for this form
  final _formKey = GlobalKey<FormState>();

  // if the password is visible or not
  bool _isPasswordVisible = false;

  @override
  Widget build(BuildContext context) {
    Size size = MediaQuery.of(context).size; // Screen size

    return Form(
      key: _formKey,
      child: Center(
        child: SizedBox(
          width: size.width * 0.8,
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: <Widget>[
              // Email field
              TextFormField(
                decoration: const InputDecoration(
                  enabledBorder: OutlineInputBorder(
                    borderSide: BorderSide(
                      color: kFormFieldColor,
                    ),
                  ),
                  focusedBorder: OutlineInputBorder(
                    borderSide: BorderSide(
                      color: kFormFieldColor,
                    ),
                  ),
                  border: OutlineInputBorder(),
                  labelStyle: kAuthStyle,
                  hintStyle: kAuthStyle,
                  labelText: 'Email',
                  hintText: 'Enter your email',
                ),
                validator: (value) {
                  if (value == null || value.isEmpty) {
                    return '* requiered';
                  }
                  RegExp emailRegex = RegExp(
                      r"^[a-zA-Z0-9.a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9]+\.[a-zA-Z]+");
                  if (!emailRegex.hasMatch(value)) {
                    return 'Enter a valid email';
                  }
                  return null;
                },
              ),

              // Password field
              Padding(
                padding: const EdgeInsets.symmetric(vertical: kDefaultPadding),
                child: TextFormField(
                  // Form field aspect
                  decoration: InputDecoration(
                    enabledBorder: const OutlineInputBorder(
                      borderSide: BorderSide(
                        color: kFormFieldColor,
                      ),
                    ),
                    focusedBorder: const OutlineInputBorder(
                      borderSide: BorderSide(
                        color: kFormFieldColor,
                      ),
                    ),
                    border: const OutlineInputBorder(),
                    labelStyle: kAuthStyle,
                    hintStyle: kAuthStyle,
                    labelText: 'Password',
                    hintText: 'Enter your password',
                    suffixIcon: IconButton(
                        // Toggle if password is visible or not
                        onPressed: () {
                          setState(() {
                            _isPasswordVisible = !_isPasswordVisible;
                          });
                        },
                        // Icon based on the state of _isPasswordVisible
                        icon: Icon(
                          _isPasswordVisible
                              ? Icons.visibility_off
                              : Icons.visibility,
                          color: kFormFieldIconColor,
                        )),
                  ),
                  obscureText: !_isPasswordVisible,
                  validator: (value) {
                    if (value == null || value.isEmpty) {
                      return '* requiered';
                    }
                    return null;
                  },
                ),
              ),

              // Submit button
              ElevatedButton(
                style: ElevatedButton.styleFrom(
                  primary: kPrimaryColor,
                  enableFeedback: true,
                ),
                onPressed: () {
                  // Check if the form is valid
                  if (_formKey.currentState!.validate()) {
                    ScaffoldMessenger.of(context).showSnackBar(
                      const SnackBar(content: Text('Logging in...')),
                    );
                  }
                },
                child: const Text(
                  'LOGIN',
                  style: kButtonTextStyle,
                ),
              ),

              TextButton(
                style: TextButton.styleFrom(
                  textStyle: kLinkStyle,
                ),
                child: const Text(
                  "Don't have an account? Sign up here",
                  style: kLinkStyle,
                ),
                onPressed: () {
                  // Go to register page
                  Navigator.push(context,
                      MaterialPageRoute(builder: (_) => RegisterScreen()));
                },
              )
            ],
          ),
        ),
      ),
    );
  }
}
