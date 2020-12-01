import React from 'react';
import { StyleSheet, Dimensions, ScrollView, TouchableWithoutFeedback, Image } from 'react-native';
import { Block, Text, theme } from 'galio-framework';

import { Images, argonTheme } from "../constants";
const { width } = Dimensions.get('window');


class Home extends React.Component {
  renderArticles = () => {
    return (
      <ScrollView
        showsVerticalScrollIndicator={false}
        contentContainerStyle={styles.articles}>
        <Block flex>
          <Block card flex style={[styles.card, styles.shadow]}>
            <TouchableWithoutFeedback>
              <Block flex style={[styles.verticalStyles, styles.shadow]}>
                <Image source={Images.iFindBanner} style={styles.fullImage} />
              </Block>
            </TouchableWithoutFeedback>
            <TouchableWithoutFeedback onPress={() => navigation.navigate(nav)}>
              <Block flex space="between" style={styles.cardDescription}>
                <Text h3 style={{ marginBottom: theme.SIZES.BASE / 2 }} color={argonTheme.COLORS.DEFAULT} >
                  Bienvenue dans la communauté !
                </Text>
                <Text size={20} color={argonTheme.COLORS.BLACK} bold>
                  iFind est la plus grande plate-forme et communauté de propriété perdue et trouvée en ligne dans le monde.{"\n\n"}
                  Notre solution de gestion des objets perdus ou des objets perdus, vous permet de trouver ce que vous avez perdu à l'université, à la gare et à tout autre endroit.
                </Text>
              </Block>
            </TouchableWithoutFeedback>
          </Block>
        </Block>
      </ScrollView>
    )
  }

  render() {
    return (
      <Block flex center style={styles.home}>
        {this.renderArticles()}
      </Block>
      
    );
  }
}

const styles = StyleSheet.create({
  home: {
    width: width,    
  },
  articles: {
    width: width - theme.SIZES.BASE * 2,
    paddingVertical: theme.SIZES.BASE,
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
  cardDescription: {
    padding : theme.SIZES.BASE / 2
  },
  imageContainer: {
    borderRadius: 3,
    elevation: 1,
    overflow: 'hidden',
  },
  image: {
     borderRadius: 3,
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
    height: 215,
    width : '100%'
  },
  shadow: {
    shadowColor: theme.COLORS.BLACK,
    shadowOffset: { width: 0, height: 2 },
    shadowRadius: 4,
    shadowOpacity: 0.1,
    elevation: 2,
  },
});

export default Home;
