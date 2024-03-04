import { useEffect, useState } from "react"
import MyAccountComponent from "../Components/MyAccountComponent"
import BackendURL from "../Utils/BackendURL";
import { useUserProfile } from "../Utils/UserProfileContextProvider";
import { Container } from "react-bootstrap";
const MyAccountPage: React.FC = () => {
    const profile = useUserProfile().userProfile;
    const [profileData, setProfileData] = useState();
    const fetcher: () => void = async () => {
        fetch(BackendURL + "Post/profileDetails/" + profile?.Id)
        .then(res => res.json()).then(json => setProfileData(json));
    }
    useEffect(() => {
        fetcher();
    }, [])
    return(<Container className="w-50">{profileData ? <MyAccountComponent profileData={profileData}/> : <>Loading...</>}</Container>)
}
export default MyAccountPage