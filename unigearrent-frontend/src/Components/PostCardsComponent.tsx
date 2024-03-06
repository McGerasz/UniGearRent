import { Card, Col, Container, Row } from "react-bootstrap";
import { PostCardData } from "../Models/PostCardData";
import { useNavigate } from "react-router-dom";

const PostCardsComponent: React.FC<{searchData: Array<PostCardData>, myPost: boolean}> = (props) => {
    const navigate = useNavigate();
    const ClickHandler: (id: number) => void = (id) => {
        navigate(props.myPost ? `/mypost/${id}` : `/post/${id}`);
    }
    return(
        <Container>{props.searchData.map((element: PostCardData) => {
            return <Row className="justify-content-center">
                <Card onClick={() => ClickHandler(element.Id)} className="h-100 mb-4 w-75" style={{border:"2px solid #d9b99b", cursor:"default"}}>
                <Card.Body>
                    <Card.Title style={{textAlign:"center", marginBottom:"0px"}}>{element.Name}</Card.Title>
                    <Container className="w-50"><hr className="mt-1 mb-2" style={{border:"1px solid black"}}></hr></Container>
                    <Card.Text  style={{textAlign:"center", lineHeight:"1.2em", height:"4.8em", overflow:"hidden", WebkitMaskImage:"linear-gradient(to bottom, black 50%, transparent 100%)", maskImage:"linear-gradient(to bottom, black 50%, transparent 100%)"}}>{element.Description}</Card.Text>
                    <Card.Text  style={{textAlign:"right"}}>{element.Location}</Card.Text>    
                </Card.Body>       
            </Card>
            </Row>
        })}</Container>
    )
}
export default PostCardsComponent;