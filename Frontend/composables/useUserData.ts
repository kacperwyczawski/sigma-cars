import {UserData} from "~/types/UserData";

export const useUserData = () => useState<UserData | null>("userData", () => null);