export interface UserData {
    userId: number;
    firstName: string;
    lastName: string;
    email: string;
    role: "admin" | "customer"
    jwt: string;
}