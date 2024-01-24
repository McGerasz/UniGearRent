import { Container } from "react-bootstrap"
import EditPostComponent from "../Components/EditPostComponent"
import { useEffect, useState } from "react";
import { PostType } from "../Models/PostType";
import { useParams } from "react-router-dom";
import BackendURL from "../Utils/BackendURL";
import GetPostdataType from "../Utils/GetPostdataType";

const EditPostPage: React.FC = () => {
    const {id} = useParams();
    const [data, setData] = useState();
    const [postType, setPostType] = useState<PostType>(PostType.Car);
    const fetcher: () => void = async () => {
        const responseData = await fetch(BackendURL + "Post/" + id).then(res =>res.json());
        setData(responseData);
        setPostType(GetPostdataType(responseData))
    }
    useEffect(() => {
        fetcher()
    }, [])
    return <Container>
        {data ? <EditPostComponent postData={data} postType={postType} /> : <>Loading...</>}    </Container>
}
export default EditPostPage;