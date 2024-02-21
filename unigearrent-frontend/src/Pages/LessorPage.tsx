import { useParams } from "react-router-dom";
import { useUserProfile } from "../Utils/UserProfileContextProvider";
import { useEffect, useState } from "react";
import BackendURL from "../Utils/BackendURL";
import { RegistrationType } from "../Models/RegistrationType";
import { Container } from "react-bootstrap";
import PostCardsComponent from "../Components/PostCardsComponent";
import { PostCardData } from "../Models/PostCardData";
import LessorPageDataComponent from "../Components/LessorPageDataComponent";

const LessorPage: React.FC = () => {
    const {id} = useParams(); 
    const profile = useUserProfile().userProfile;
    const [lessorData, setLessorData] = useState();
    const fetcher: () => void = async () => {
        await fetch(BackendURL + "Post/lessorPageData/" + id + (profile?.Type === RegistrationType.User ? "?userName=" + profile.Username : ""))
        .then(res => res.json()).then(json => setLessorData(json));
    }
    useEffect(() => {
        fetcher();
    }, [])
    return(<Container>{
        lessorData ? 
        <LessorPageDataComponent lessorData={lessorData} />
        : <h1>Loading...</h1>
        }</Container>)
}
export default LessorPage;