import React, {useState, useEffect} from 'react';
import { StyleSheet, Dimensions, ScrollView, TouchableWithoutFeedback, Image, AsyncStorage, ActivityIndicator, View, TextInput } from 'react-native';
import Axios from "axios";
import { Block, Text, theme } from 'galio-framework';

import { Images, argonTheme } from "../constants";
const { width, height } = Dimensions.get('window');
import { GiftedChat, TitleMessage } from 'react-native-gifted-chat'


const ItemDetails = (props) => {
    const [item, setItem] = useState({item: null, user: null})
    const [comments, setComments] = useState(null)
    const [isLoading, setLoading] = useState(true)
    const [commentLoading, setCommentLoading] = useState(false)
    useEffect(() => {
        console.log(props)
        getItem()
    }, []);

    const getItem= async () => {
        const token = await AsyncStorage.getItem('jwt')
        Axios.get("https://ifind-lsti.herokuapp.com/api/v1/lost/"+props.route.params.id, {
        headers: {
            Authorization: 'Bearer ' + token
        }
        })
        .then(res => {
            setItem({item: res.data.item[0], user: res.data.owner})
            setLoading(false)
            getComments(res.data.item[0].comments)
            //console.log(res.data.item[0].comments)
        })
        .catch(err => {
            console.log(err)
        })
    }

    const getComments = async (comments) => {
        const token = await AsyncStorage.getItem('jwt')
        var requests = comments.map((comment)=> {
            return Axios.get("https://ifind-auth.herokuapp.com/api/v1/users/"+comment.userId, {
            headers: {
                Authorization: 'Bearer ' + token
            }
            })
            .then(res => {
                comment.firstName = res.data.firstName
                comment.lastName = res.data.lastName
                comment.email = res.data.email
                return comment
            })
            .catch(err => {
                console.log(err)
            })
        })
        Promise.all(requests).then((value) => setComments(value))
    }

    const sendComment = async (comment) => {
        setCommentLoading(true)
        const token = await AsyncStorage.getItem('jwt')
        Axios.post("https://ifind-lsti.herokuapp.com/api/v1/lost/"+props.route.params.id+"/comments",{description: comment}, {
            headers: {
                Authorization: 'Bearer ' + token
            }
            })
            .then(res => {
                setCommentLoading(false)
                getItem()
            })
            .catch(err => {
                console.log(err)
            })
    }

    const renderComments = () => {
        if(Array.isArray(comments) && comments.length>0) { return (
            <Block flex>
                <Text h6 style={{margin : 6}} color={argonTheme.COLORS.DEFAULT} > Commentaires </Text>
                {comments.map((comment)=> {
                return (
                <Block key={comment.Id} card flex style={[styles.card, styles.shadow]}>
                    <Block flex style={styles.cardDescription}>
                        <Text style={styles.attributes} color={argonTheme.COLORS.DEFAULT} bold>{comment.firstName+ " "+comment.lastName+" ("+Date.parse(comment.createdAt).toString("dd-MM-yyyy h:MM:ss tt")+")"} </Text>
                        <Text style={styles.contact} bold>{comment.email} </Text>
                        <Text style={styles.attributes} color={argonTheme.COLORS.BLACK}>{comment.description} </Text>
                    </Block>
                </Block>
                )
                })}
            </Block> )
        }
        else if(Array.isArray(comments) && comments.length==0) { 
            return(<Text h6 style={{margin : 6}} color={argonTheme.COLORS.DEFAULT} > Aucun commentaire </Text>) 
        }
        else {
            return(<ActivityIndicator style={styles.activityIndicator} color={argonTheme.COLORS.DEFAULT} size="large" />) 
        }
    }

    const renderArticles = () => {
        return (
        <ScrollView
            showsVerticalScrollIndicator={false}
            contentContainerStyle={styles.articles}>
            <Block flex>
            <Block card flex style={[styles.card, styles.shadow]}>
                <TouchableWithoutFeedback>
                <Block flex style={[styles.verticalStyles, styles.shadow]}>
                    <Image source={{uri: item.item.photo}} style={styles.fullImage} />
                </Block>
                </TouchableWithoutFeedback>
                <TouchableWithoutFeedback>
                <Block flex space="between" style={styles.cardDescription}>
                    <Text h6 style={{margin : 6}} color={argonTheme.COLORS.DEFAULT} >
                        {item.item.name}
                    </Text>
                    <Text style={styles.attributes} color={argonTheme.COLORS.BLACK} >Propriétaire : {item.user.firstName + " " +item.user.lastName}</Text>
                    <Text style={styles.attributes} color={argonTheme.COLORS.BLACK} >Contact : {item.user.email}</Text>
                    <Text style={styles.attributes} color={argonTheme.COLORS.BLACK} >Date : {Date.parse(item.item.lostAt).toString("dd-MM-yyyy h:MM:ss tt")}</Text>
                    <Text style={styles.attributes} color={argonTheme.COLORS.BLACK} >Modèle : {item.item.model}</Text>
                    <Text style={styles.attributes} color={argonTheme.COLORS.BLACK} >Couleur : {item.item.color}</Text>
                    <Text style={styles.attributes} color={argonTheme.COLORS.BLACK} >Description : {"\n"+item.item.description}</Text>
                    <View style={{ flexDirection: "row" }}>
                        <Text style={styles.attributes} color={argonTheme.COLORS.BLACK} bold>Etat de la réclamation : </Text>
                        {!item.item.found && <Text style={styles.attributes} color={argonTheme.COLORS.ERROR} bold>En cours de traitement</Text>}
                        {item.item.found && <Text style={styles.attributes} color={argonTheme.COLORS.SUCCESS} bold>Objet trouvé !</Text>}
                    </View>
                </Block>
                </TouchableWithoutFeedback>
            </Block>
            </Block>
            {renderComments()}

        </ScrollView>
        )
    }
    return (
      <>
        <Block flex center style={styles.home}>
            {!isLoading && item!=null ? renderArticles(): 
                <ActivityIndicator style={styles.activityIndicator} color={argonTheme.COLORS.DEFAULT} size="large" />
            }
        </Block>
        <GiftedChat
            onSend={(value) => {sendComment(value[0].text)}}
        />
      </>
    );
}

const styles = StyleSheet.create({
  home: {
    width: width,    
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    minHeight: "90%"
  },
  articles: {
    width: width - theme.SIZES.BASE * 2,
    paddingVertical: theme.SIZES.BASE,
  },
  card: {
    backgroundColor: theme.COLORS.WHITE,
    marginVertical: theme.SIZES.BASE,
    borderWidth: 0,
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
  attributes : {
    fontSize: 14,
    marginBottom: 4
  },
  contact: {
      fontSize: 12,
      color: "grey"
  },
  newInput: {
    borderWidth: 1,
    borderColor: '#ccc',
    fontSize: 16,
    padding:10,
    height:50,
  },
});

export default ItemDetails;
