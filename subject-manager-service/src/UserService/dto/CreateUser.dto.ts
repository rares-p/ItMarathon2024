import {UserRole} from "../user.entity";
import {IsEnum, IsString} from "class-validator";

export class CreateUserDto {
    @IsString()
    username: string;

    @IsString()
    password: string;
}