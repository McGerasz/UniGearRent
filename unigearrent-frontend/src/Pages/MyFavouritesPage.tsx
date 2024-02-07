import { PostCardData } from "../Models/PostCardData";
import { useUserProfile } from "../Utils/UserProfileContextProvider";
import { useEffect, useState } from "react";
import BackendURL from "../Utils/BackendURL";
import { Container } from "react-bootstrap";
import PostCardsComponent from "../Components/PostCardsComponent";

const MyFavouritesPage: React.FC = () => {
    const [searchData, setSearchData] = useState<Array<PostCardData>>(new Array<PostCardData>());
    const userName =  useUserProfile().userProfile?.Username;
    useEffect(() => {
        setSearchData(new Array<PostCardData>());
        const fetcher: () => void = async () => {
            let response = await fetch(BackendURL+"Post/getFavourites/" + userName).then(res => res.json());
            const processedArray = response.map((element: any) => new PostCardData(element.id, element.name, element.location, element.posterId, element.description));
            setSearchData(processedArray as Array<PostCardData>);
        }

        fetcher();
    }, [])
    return(
    <>
    <Container className="text-center mb-1 mt-5"><h1>Your favourite posts</h1></Container>
    <Container className="justify-content-md-center w-75 mt-5">
        {searchData.length > 0 ? <PostCardsComponent searchData={searchData} myPost={false}/> : <></>}
    </Container>
    </>)
}
export default MyFavouritesPage;