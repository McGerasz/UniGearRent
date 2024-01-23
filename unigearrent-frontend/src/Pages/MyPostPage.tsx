import { Container } from "react-bootstrap"
import { useUserProfile } from "../Utils/UserProfileContextProvider"
import { RegistrationType } from "../Models/RegistrationType";
import { useParams } from "react-router-dom";
import MyPostComponent from "../Components/MyPostComponent";

const MyPostPage: React.FC = () => {
    const {id} = useParams(); 
    const profile = useUserProfile().userProfile;
    return(<Container className="w-75">{profile?.Type === RegistrationType.Lessor ? (<MyPostComponent id={parseInt(id as string)}/>) : (<Container fluid><h1>You're not supposed to be here</h1></Container>)}</Container>)
}
export default MyPostPage