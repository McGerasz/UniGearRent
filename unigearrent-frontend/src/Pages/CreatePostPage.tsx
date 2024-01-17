import { useState } from "react";
import CreatePostComponent from "../Components/CreatePostComponent";
import { PostType } from "../Models/PostType";
import PostTypeComponent from "../Components/PostTypeComponent";

const CreatePostPage: React.FC = () => {
    const [postType, setPostType] = useState<PostType>();
    return (<>{postType ? <CreatePostComponent postType={postType}/> : (<PostTypeComponent postTypeSetter={setPostType}/>)}</>)
}
export default CreatePostPage;