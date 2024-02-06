import { useEffect, useState } from "react";
import BackendURL from "../Utils/BackendURL";
import GetPostdataType from "../Utils/GetPostdataType";
import { PostType } from "../Models/PostType";
import { Container } from "react-bootstrap";
import PostElement from "./PostElement";

const MyPostComponent: React.FC<{id: number}> = (props) => {
    const [PostData, setPostData] = useState(null);
    const [PostDataType, setPostDataType] = useState<PostType>(PostType.Car);
    const [PosterName, setPosterName] = useState<string>("");
    const fetcher: () => void = async () => {
        const data = await fetch(BackendURL + "Post/" + props.id).then(async res => await res.json());
        setPostData(data);
        setPostDataType(GetPostdataType(data));
        const posterData = await fetch(BackendURL + "Post/lessorDetails/" + props.id).then(async res => await res.json());
        setPosterName(posterData["name"]);
    }
    useEffect(() => {
        fetcher();
    }, [])
    return (PostData ? (<PostElement PostData={PostData} PostDataType={PostDataType} MyPost={true} PosterName={PosterName}/>) : <Container fluid><h1>Loading data...</h1></Container>)
}
export default MyPostComponent;