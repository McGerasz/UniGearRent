import { Button, Card, Col, Container, Row } from "react-bootstrap";
import { useUserProfile } from "../Utils/UserProfileContextProvider";
import { RegistrationType } from "../Models/RegistrationType";

const WelcomeComponent: React.FC = () => {
    const profile = useUserProfile().userProfile;
    return( <Container fluid className="d-flex align-items-center justify-content-center" style={{position:"absolute", margin:"auto", height:"90%"}}>
        <header className="text-white text-center py-5 w-100" style={{backgroundColor:"#b08c74"}}>
          <Container>
            <h1>Welcome to UniGearRent</h1>
            <p>
              Your one-stop destination for renting cars and trailers, or making
              money by renting out yours!
            </p>
            <p>
                {profile ? (profile.Type === RegistrationType.Lessor ? `Navigate to "My Posts to begin creating a post or to manage your existing ones!` : "Have you checked out the favourites feature?") : "Login or create an account to gain access to more features!"}
            </p>
          </Container>
        </header>
      </Container>
  
    );
}
export default WelcomeComponent;