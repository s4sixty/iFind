import React, {useState} from "react";
import {
  StyleSheet,
  ImageBackground,
  Dimensions,
  StatusBar,
  KeyboardAvoidingView,
  Image,
  Alert,
  AsyncStorage
} from "react-native";
import { Block, Checkbox, Text, theme } from "galio-framework";

import { Button, Icon, Input } from "../components";
import { Images, argonTheme } from "../constants";
import axios from "axios";

const { width, height } = Dimensions.get("screen");

const Register = (props) => {

  const [isLoading, setLoading] = React.useState(true)
  const [firstName, setFirstName] = React.useState(null);
  const [lastName, setLastName] = React.useState(null);
  const [email, setEmail] = React.useState(null);
  const [error, setError] = React.useState('');
  const [password, setPassword] = React.useState(null);
  async function signIn(){
    //props.navigation.navigate("App")
    if(email!=null && password!=null) {
      axios.post(`https://ifind-gtw.herokuapp.com/api/v1/register`, {firstName, lastName, email, password })
      .then(async res => {
        try {
          await AsyncStorage.setItem('jwt',res.data.token)
          console.log(res.data.token)
          Alert.alert('Bienvenue '+res.data.user.firstName.charAt(0).toUpperCase() + res.data.user.firstName.slice(1)+' !')
          props.navigation.navigate("App")
        } catch(e) {
          console.log(e)
        }
      })
      .catch(err => {
        console.log(err)
        setError("Vos identifiants sont incorrects.")
      })
    } else {
      setError("Veuillez renseignez vos identifiants.")
      console.log(error)
    }
  }

    return (
      <Block flex middle>
        <StatusBar hidden />
        <ImageBackground
          source={Images.RegisterBackground}
          style={{ width, height, zIndex: 1 }}
        >
          <Block flex middle>
            <Block style={styles.registerContainer}>
              <Block flex={0.20} middle style={styles.socialConnect}>
                <Block center>
                  <Image source={Images.iFindLogo} style={styles.logo} />
                </Block>
                <Text color="#8898AA" size={12}>
                  Bienvenue sur iFind
                </Text>
              </Block>
              <Block flex>
                <Block flex={0.17} middle>
                  <Text color="#8898AA" size={12}>
                    Créez un compte pour démarrer
                  </Text>
                </Block>
                <Block flex center>
                  <KeyboardAvoidingView
                    style={{ flex: 1 }}
                    behavior="padding"
                    enabled
                  >
                    <Block width={width * 0.8} style={{ marginBottom: 5 }}>
                      <Input
                        borderless
                        placeholder="Prénom"
                        onChangeText={(data) => setFirstName(data)}
                        iconContent={
                          <Icon
                            size={16}
                            color={argonTheme.COLORS.ICON}
                            name="ic_mail_24px"
                            family="ArgonExtra"
                            style={styles.inputIcons}
                          />
                        }
                      />
                    </Block>
                    <Block width={width * 0.8} style={{ marginBottom: 5 }}>
                      <Input
                        borderless
                        placeholder="Nom"
                        onChangeText={(data) => setLastName(data)}
                        iconContent={
                          <Icon
                            size={16}
                            color={argonTheme.COLORS.ICON}
                            name="ic_mail_24px"
                            family="ArgonExtra"
                            style={styles.inputIcons}
                          />
                        }
                      />
                    </Block>
                    <Block width={width * 0.8} style={{ marginBottom: 5 }}>
                      <Input
                        borderless
                        placeholder="Email"
                        onChangeText={(data) => setEmail(data)}
                        iconContent={
                          <Icon
                            size={16}
                            color={argonTheme.COLORS.ICON}
                            name="ic_mail_24px"
                            family="ArgonExtra"
                            style={styles.inputIcons}
                          />
                        }
                      />
                    </Block>
                    <Block width={width * 0.8}>
                      <Input
                        password
                        borderless
                        placeholder="Password"
                        onChangeText={(data) => setPassword(data)}
                        iconContent={
                          <Icon
                            size={16}
                            color={argonTheme.COLORS.ICON}
                            name="padlock-unlocked"
                            family="ArgonExtra"
                            style={styles.inputIcons}
                          />
                        }
                      />
                      <Block row style={styles.passwordCheck}>
                        <Text size={12} color={argonTheme.COLORS.ERROR} >
                          {error}
                        </Text>
                      </Block>
                    </Block>
                    <Block row width={width * 0.75}>
                      <Checkbox
                        checkboxStyle={{
                          borderWidth: 3
                        }}
                        color={argonTheme.COLORS.PRIMARY}
                        label="J'accepte les"
                      />
                      <Button
                        style={{ width: 150 }}
                        color="transparent"
                        textStyle={{
                          color: argonTheme.COLORS.PRIMARY,
                          fontSize: 14
                        }}
                      >
                        conditions d'utilisation
                      </Button>
                    </Block>
                    <Block middle>
                      <Button 
                      color="primary" 
                      style={styles.createButton}
                      onPress={() => signIn()}
                      >
                        <Text bold size={14} color={argonTheme.COLORS.WHITE}>
                          S'authentifier
                        </Text>
                      </Button>
                    </Block>
                    <Block middle>
                      <Button 
                      color="secondary" 
                      style={styles.createButton}
                      onPress={() => props.navigation.navigate("Register")}
                      >
                        <Text bold size={14} color={argonTheme.COLORS.BLACK}>
                          Créer un compte
                        </Text>
                      </Button>
                    </Block>
                  </KeyboardAvoidingView>
                </Block>
              </Block>
            </Block>
          </Block>
        </ImageBackground>
      </Block>
    );
}

const styles = StyleSheet.create({
  registerContainer: {
    width: width * 0.9,
    height: height * 0.9,
    backgroundColor: "#F4F5F7",
    borderRadius: 4,
    shadowColor: argonTheme.COLORS.BLACK,
    shadowOffset: {
      width: 0,
      height: 4
    },
    shadowRadius: 8,
    shadowOpacity: 0.1,
    elevation: 1,
    overflow: "hidden"
  },
  socialConnect: {
    backgroundColor: argonTheme.COLORS.WHITE,
    borderBottomWidth: StyleSheet.hairlineWidth,
    borderColor: "#8898AA"
  },
  socialButtons: {
    width: 120,
    height: 40,
    backgroundColor: "#fff",
    shadowColor: argonTheme.COLORS.BLACK,
    shadowOffset: {
      width: 0,
      height: 4
    },
    shadowRadius: 8,
    shadowOpacity: 0.1,
    elevation: 1
  },
  socialTextButtons: {
    color: argonTheme.COLORS.PRIMARY,
    fontWeight: "800",
    fontSize: 14
  },
  inputIcons: {
    marginRight: 12
  },
  passwordCheck: {
    paddingLeft: 15,
    paddingTop: 13,
    paddingBottom: 30
  },
  createButton: {
    width: width * 0.5,
    marginTop: 25
  },
  logo: {
    width: 200,
    height: 60,
  },
});

export default Register;
