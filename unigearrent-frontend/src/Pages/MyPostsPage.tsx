import { Button, Container } from "react-bootstrap"
import { useEffect, useState } from "react"
import { PostCardData } from "../Models/PostCardData";
import PostCardsComponent from "../Components/PostCardsComponent";
import BackendURL from "../Utils/BackendURL";
import { useUserProfile } from "../Utils/UserProfileContextProvider";
import { useNavigate } from "react-router-dom";

const MyPostsPage: React.FC = () => {
    const [searchData, setSearchData] = useState<Array<PostCardData>>(new Array<PostCardData>());
    const userName =  useUserProfile().userProfile?.Username;
    const navigate = useNavigate();
    useEffect(() => {
        setSearchData(new Array<PostCardData>());
        const fetcher: () => void = async () => {
            let response = await fetch(BackendURL+"Post/byUser/" + userName).then(res => res.json());
            const processedArray = response.map((element: any) => new PostCardData(element.id, element.name, element.location, element.posterId, element.description));
            setSearchData(processedArray as Array<PostCardData>);
        }

        fetcher();
    }, [])
    const ClickHandler = () => {
        navigate("/createpost");
    }
    return(
    <Container className="justify-content-md-center w-75 mt-5">
        <Container className="w-100 d-flex justify-content-center mb-5">
            <Button className="btn-dark w-75" onClick={ClickHandler}>Create post</Button>
        </Container>
        {searchData.length > 0 ? <PostCardsComponent searchData={searchData}/> : <></>}
    </Container>)
}
export default MyPostsPage;