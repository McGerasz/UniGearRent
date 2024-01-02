import { useContext, useState, createContext, ReactNode } from "react";
import { LessorProfile } from "../Models/LessorProfile";
import { UserProfile } from "../Models/UserProfile";
interface UserProfileContextType {
    userProfile: LessorProfile | UserProfile | null;
    setUserProfile: (profile: LessorProfile | UserProfile | null) => void;
}

const UserProfileContext = createContext<UserProfileContextType | undefined>(undefined);
export const UserProfileProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [userProfile, setUserProfile] = useState<LessorProfile | UserProfile | null>(null);
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