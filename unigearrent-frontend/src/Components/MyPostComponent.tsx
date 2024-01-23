import { useEffect, useState } from "react";
import BackendURL from "../Utils/BackendURL";
import GetPostdataType from "../Utils/GetPostdataType";
import { PostType } from "../Models/PostType";
import { Container } from "react-bootstrap";
import PostElement from "./PostElement";

const MyPostComponent: React.FC<{id: number}> = (props) => {
    const [PostData, setPostData] = useState(null);
    const [PostDataType, setPostDataType] = useState<PostType>(PostType.Car);
    const fetcher: () => void = async () => {
        const data = await fetch(BackendURL + "Post/" + props.id).then(async res => await res.json());
        setPostData(data);
        setPostDataType(GetPostdataType(data));
    }
    useEffect(() => {
        fetcher();
    }, [])
    return (PostData ? (<PostElement PostData={PostData} PostDataType={PostDataType}/>) : <Container fluid><h1>Loading data...</h1></Container>)
}
export default MyPostComponent;