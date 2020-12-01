import React, {useEffect, useState} from "react";
import {
  StyleSheet,
  Dimensions,
  ScrollView,
  Image,
  ImageBackground,
  Platform,
  AsyncStorage,
  ActivityIndicator
} from "react-native";
import { Block, Text, theme } from "galio-framework";

import { Button } from "../components";
import { Images, argonTheme } from "../constants";
import { HeaderHeight } from "../constants/utils";
import Axios from "axios";

const { width, height } = Dimensions.get("screen");

const thumbMeasure = (width - 48 - 32) / 3;

const Profile = (props) => {
  const [data, setData] = useState({email:null, firstName:null, lastName:null})
  const [isLoading, setLoading] = useState(true)
  useEffect(() => {
    getProfile()
  }, []);

  const getProfile = async () => {
    const token = await AsyncStorage.getItem('jwt')
    Axios.get("https://ifind-gtw.herokuapp.com/api/v1/users", {
      headers: {
        Authorization: 'Bearer ' + token
      }
    })
    .then(res => {
      setData({
        email: res.data.email,
        firstName: res.data.firstName,
        lastName: res.data.lastName
      })
      setLoading(false)
      console.log(data)
    })
    .catch(err => {
      console.log(err)
    })
  }

  return (
    <Block flex style={styles.profile}>
      <Block flex>
        <ImageBackground
          source={Images.ProfileBackground}
          style={styles.profileContainer}
          imageStyle={styles.profileBackground}
        >
          <ScrollView
            showsVerticalScrollIndicator={false}
            style={{ width, marginTop: '25%' }}
          >
            <Block flex style={styles.profileCard}>
              <Block middle style={styles.avatarContainer}>
                <Image
                  source={{ uri: Images.ProfilePicture }}
                  style={styles.avatar}
                />
              </Block>
              <Block style={styles.info}>
                <Block
                  middle
                  row
                  space="evenly"
                  style={{ marginTop: 20, paddingBottom: 24 }}
                >
                  <Button
                    small
                    style={{ backgroundColor: argonTheme.COLORS.DEFAULT }}
                  >
                    Modifier
                  </Button>
                </Block>
                <Block row space="evenly">
                  <Block middle>
                    <Text
                      bold
                      size={18}
                      color="#525F7F"
                      style={{ marginBottom: 4 }}
                    >
                      0
                    </Text>
                    <Text size={12} color={argonTheme.COLORS.TEXT}>Objets perdus</Text>
                  </Block>
                  <Block middle>
                    <Text
                      bold
                      color="#525F7F"
                      size={18}
                      style={{ marginBottom: 4 }}
                    >
                      0
                    </Text>
                    <Text size={12} color={argonTheme.COLORS.TEXT}>Objets trouvés</Text>
                  </Block>
                </Block>
              </Block>
              {!isLoading ? 
              <Block flex>
                <Block middle style={styles.nameInfo}>
                  <Text bold size={28} color="#32325D">
                    {data.firstName.charAt(0).toUpperCase() + data.firstName.slice(1)+' '+data.lastName.charAt(0).toUpperCase() + data.lastName.slice(1)}
                  </Text>
                  <Text size={16} color="#32325D" style={{ marginTop: 10 }}>
                    {data.email}
                  </Text>
                </Block>
                <Block middle style={{ marginTop: 30, marginBottom: 16 }}>
                  <Block style={styles.divider} />
                </Block>
                <Block middle>
                  <Text
                    size={16}
                    color="#525F7F"
                    style={{ textAlign: "center" }}
                  >
                    Membre actif dans la communauté iFind.
                  </Text>
                  <Button
                    color="transparent"
                    textStyle={{
                      color: "#233DD2",
                      fontWeight: "500",
                      fontSize: 16
                    }}
                    onPress={()=> {props.navigation.navigate("Mes réclamations")}}
                  >
                    Afficher mes réclamations
                  </Button>
                  
                </Block>
              </Block>
              : <ActivityIndicator style={styles.activityIndicator} color={argonTheme.COLORS.DEFAULT} size="large" />}
            </Block>
            
          </ScrollView>
        </ImageBackground>
      </Block>
    </Block>
  );
}

const styles = StyleSheet.create({
  activityIndicator: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    height: 80
  },
  profile: {
    marginTop: Platform.OS === "android" ? -HeaderHeight : 0,
    // marginBottom: -HeaderHeight * 2,
    flex: 1
  },
  profileContainer: {
    width: width,
    height: height,
    padding: 0,
    zIndex: 1
  },
  profileBackground: {
    width: width,
    height: height / 2
  },
  profileCard: {
    // position: "relative",
    padding: theme.SIZES.BASE,
    marginHorizontal: theme.SIZES.BASE,
    marginTop: 65,
    borderTopLeftRadius: 6,
    borderTopRightRadius: 6,
    backgroundColor: theme.COLORS.WHITE,
    shadowColor: "black",
    shadowOffset: { width: 0, height: 0 },
    shadowRadius: 8,
    shadowOpacity: 0.2,
    zIndex: 2
  },
  info: {
    paddingHorizontal: 40
  },
  avatarContainer: {
    position: "relative",
    marginTop: -80
  },
  avatar: {
    width: 124,
    height: 124,
    borderRadius: 62,
    borderWidth: 0
  },
  nameInfo: {
    marginTop: 35
  },
  divider: {
    width: "90%",
    borderWidth: 1,
    borderColor: "#E9ECEF"
  },
  thumb: {
    borderRadius: 4,
    marginVertical: 4,
    alignSelf: "center",
    width: thumbMeasure,
    height: thumbMeasure
  }
});

export default Profile;
