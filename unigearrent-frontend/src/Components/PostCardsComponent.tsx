import { Card, Col, Row } from "react-bootstrap";
import { PostCardData } from "../Models/PostCardData";
import { useNavigate } from "react-router-dom";

const PostCardsComponent: React.FC<{searchData: Array<PostCardData>}> = (props) => {
    const navigate = useNavigate();
    const ClickHandler: (id: number) => void = (id) => {
        navigate(`/Post/${id}`);
    }
    return(
        <Row sm={1} md={2} lg={4}>{props.searchData.map((element: PostCardData) => {
            console.log(element)
            return <Col>
                <Card onClick={() => ClickHandler(element.Id)} className="h-100">
                <Card.Body>
                    <Card.Title style={{textAlign:"center"}}>{element.Name}</Card.Title>
                    <Card.Text  style={{textAlign:"center", lineHeight:"1.2em", height:"4.8em", overflow:"hidden", WebkitMaskImage:"linear-gradient(to bottom, black 50%, transparent 100%)", maskImage:"linear-gradient(to bottom, black 50%, transparent 100%)"}}>{element.Description}</Card.Text>
                    <Card.Text  style={{textAlign:"center"}}>{element.Location}</Card.Text>    
                </Card.Body>       
            </Card>
            </Col>
        })}</Row>
    )
}
export default PostCardsComponent;