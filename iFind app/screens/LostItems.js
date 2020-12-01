import React, {useState, useEffect} from "react";
import {
  ScrollView,
  StyleSheet,
  Image,
  TouchableWithoutFeedback,
  Dimensions,
  AsyncStorage,
  ActivityIndicator
} from "react-native";
//galio
import { Block, Text, theme } from "galio-framework";
//argon
import { argonTheme } from "../constants/";
import Axios from "axios";
require('datejs');

const { width } = Dimensions.get("screen");

const thumbMeasure = (width - 48 - 32) / 3;
const cardWidth = width - theme.SIZES.BASE * 2;

const LostItems = (props) => {
  const [items, setItems] = useState([])
  const [isLoading, setLoading] = useState(true)
  useEffect(() => {
    getLostItems()
  }, []);

  const getLostItems= async () => {
    const token = await AsyncStorage.getItem('jwt')
    Axios.get("https://ifind-lsti.herokuapp.com/api/v1/lost/all", {
      headers: {
        Authorization: 'Bearer ' + token
      }
    })
    .then(res => {
      setItems(res.data)
      setLoading(false)
      //console.log(res.data)
    })
    .catch(err => {
      console.log(err)
    })
  }

  const renderCards = () => {
    return (
      <Block flex style={styles.group}>
        <Block flex>
          <Block style={{ paddingHorizontal: theme.SIZES.BASE }}>
            {!isLoading && items.length>0 ? items.map((item)=> {
              //{console.log(item)}
              return (
              <Block key={item.id} row card flex style={[styles.card, styles.shadow]}>
                <TouchableWithoutFeedback onPress={() => props.navigation.navigate("ItemDetails",{id : item.id} )}>
                  <Block flex style={styles.horizontalStyles}>
                    <Image source={{uri: item.photo}} style={styles.horizontalImage} />
                  </Block>
                </TouchableWithoutFeedback>
                <TouchableWithoutFeedback onPress={() => props.navigation.navigate("ItemDetails", {id : item.id})}>
                  <Block flex space="evenly" style={styles.cardDescription}>
                    <Text size={14} style={styles.cardTitle}>{item.name}</Text>
                    <Text size={12} style={styles.cardContent}>Modèle : {item.model}</Text>
                    <Text size={12} style={styles.cardContent}>Date de perte : {Date.parse(item.lostAt).toString("dd-mm-yyyy")}</Text>
                    <Text size={12} style={styles.cardContent}>Localisation : {item.city}</Text>
                    <Text size={12} color={argonTheme.COLORS.ACTIVE} bold>Plus d'infos</Text>
                  </Block>
                </TouchableWithoutFeedback>
              </Block>
              )
            }): <ActivityIndicator style={styles.activityIndicator} color={argonTheme.COLORS.DEFAULT} size="large" />}
          </Block>
        </Block>
      </Block>
    );
  };
  return (
    <Block flex center>
      <ScrollView showsVerticalScrollIndicator={false} contentContainerStyle={styles.articles}>
        {renderCards()}
      </ScrollView>
    </Block>
  );
}

const styles = StyleSheet.create({
  articles: {
    width: width - theme.SIZES.BASE * 2,
    paddingVertical: theme.SIZES.BASE,
  },
  title: {
    paddingBottom: theme.SIZES.BASE,
    paddingHorizontal: theme.SIZES.BASE * 2,
    marginTop: 22,
    color: argonTheme.COLORS.HEADER
  },
  group: {
    paddingTop: theme.SIZES.BASE
  },
  albumThumb: {
    borderRadius: 4,
    marginVertical: 4,
    alignSelf: "center",
    width: thumbMeasure,
    height: thumbMeasure
  },
  category: {
    backgroundColor: theme.COLORS.WHITE,
    marginVertical: theme.SIZES.BASE / 2,
    borderWidth: 0
  },
  categoryTitle: {
    height: "100%",
    paddingHorizontal: theme.SIZES.BASE,
    backgroundColor: "rgba(0, 0, 0, 0.5)",
    justifyContent: "center",
    alignItems: "center"
  },
  imageBlock: {
    overflow: "hidden",
    borderRadius: 4
  },
  productItem: {
    width: cardWidth - theme.SIZES.BASE * 2,
    marginHorizontal: theme.SIZES.BASE,
    shadowColor: "black",
    shadowOffset: { width: 0, height: 7 },
    shadowRadius: 10,
    shadowOpacity: 0.2
  },
  productImage: {
    width: cardWidth - theme.SIZES.BASE,
    height: cardWidth - theme.SIZES.BASE,
    borderRadius: 3
  },
  productPrice: {
    paddingTop: theme.SIZES.BASE,
    paddingBottom: theme.SIZES.BASE / 2
  },
  productDescription: {
    paddingTop: theme.SIZES.BASE
    // paddingBottom: theme.SIZES.BASE * 2,
  },
  card: {
    backgroundColor: theme.COLORS.WHITE,
    marginVertical: theme.SIZES.BASE,
    borderWidth: 0,
    minHeight: 114,
    marginBottom: 16
  },
  cardTitle: {
    flex: 1,
    flexWrap: 'wrap',
    paddingBottom: 6
  },
  cardContent: {
    flex: 1,
    flexWrap: 'wrap',
    paddingBottom: 2
  },
  cardDescription: {
    padding: theme.SIZES.BASE / 2
  },
  imageContainer: {
    borderRadius: 3,
    elevation: 1,
    overflow: 'hidden',
  },
  image: {
    // borderRadius: 3,
  },
  horizontalImage: {
    height: 122,
    width: 'auto',
  },
  horizontalStyles: {
    borderTopRightRadius: 0,
    borderBottomRightRadius: 0,
  },
  verticalStyles: {
    borderBottomRightRadius: 0,
    borderBottomLeftRadius: 0
  },
  fullImage: {
    height: 215
  },
  shadow: {
    shadowColor: theme.COLORS.BLACK,
    shadowOffset: { width: 0, height: 2 },
    shadowRadius: 4,
    shadowOpacity: 0.1,
    elevation: 2,
  },
});

export default LostItems;
