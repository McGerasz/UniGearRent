import { Container } from "react-bootstrap"
import PostElement from "../Components/PostElement"
import { useState, useEffect } from "react";
import { PostType } from "../Models/PostType";
import BackendURL from "../Utils/BackendURL";
import GetPostdataType from "../Utils/GetPostdataType";
import { useParams } from "react-router-dom";

const PostPage: React.FC = () => {
    const {id} = useParams();
    const [PostData, setPostData] = useState(null);
    const [PostDataType, setPostDataType] = useState<PostType>(PostType.Car);
    const fetcher: () => void = async () => {
        const data = await fetch(BackendURL + "Post/" + id).then(async res => await res.json());
        setPostData(data);
        setPostDataType(GetPostdataType(data));
    }
    useEffect(() => {
        fetcher();
    }, [])
    return(<Container className="w-75">{PostData ? <PostElement PostData={PostData} PostDataType={PostDataType} MyPost={false} /> : <h1>Loading...</h1>}</Container>)
}
export default PostPage