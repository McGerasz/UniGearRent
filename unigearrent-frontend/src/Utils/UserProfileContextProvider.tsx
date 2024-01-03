import { useContext, useState, createContext, ReactNode } from "react";
import { LessorProfile } from "../Models/LessorProfile";
import { UserProfile } from "../Models/UserProfile";
import { Profile } from "../Models/Profile";
interface UserProfileContextType {
    userProfile: Profile | LessorProfile | UserProfile | null;
    setUserProfile: (profile: Profile | LessorProfile | UserProfile | null) => void;
}

const UserProfileContext = createContext<UserProfileContextType | undefined>(undefined);
export const UserProfileProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [userProfile, setUserProfile] = useState<Profile | LessorProfile | UserProfile | null>(null);
    return (
      <UserProfileContext.Provider value={{ userProfile, setUserProfile }}>
        {children}
      </UserProfileContext.Provider>
    );
  };
  export const useUserProfile = () => {
    const context = useContext(UserProfileContext);
    if (!context) {
      throw new Error('useUserProfile must be used within a UserProfileProvider');
    }
    return context;
  };