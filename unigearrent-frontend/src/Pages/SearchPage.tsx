import { Container } from "react-bootstrap"
import { useState } from "react"
import PostSearchComponent from "../Components/PostSearchComponent"

const SearchPage: React.FC = () => {
    return(<Container className="justify-content-md-center w-75 mt-5">
        <PostSearchComponent />
    </Container>)
}
export default SearchPage