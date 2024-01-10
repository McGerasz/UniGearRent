import { Container } from "react-bootstrap"
import { useState } from "react"
import PostSearchComponent from "../Components/PostSearchComponent"
import { PostCardData } from "../Models/PostCardData";
import PostCardsComponent from "../Components/PostCardsComponent";

const SearchPage: React.FC = () => {
    const [searchData, setSearchData] = useState<Array<PostCardData>>(new Array<PostCardData>());
    return(
    <Container className="justify-content-md-center w-75 mt-5">
        <PostSearchComponent searchDataSetter={setSearchData}/>
        {searchData.length > 0 ? <PostCardsComponent searchData={searchData}/> : <>bar</>}
    </Container>)
}
export default SearchPage