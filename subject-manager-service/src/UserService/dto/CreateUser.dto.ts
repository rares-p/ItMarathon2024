import {IsEnum, IsNotEmpty, IsNumber, IsOptional, IsString} from "class-validator";
import {Years} from "../entities/student.entity";

export class CreateUserDto {
    @IsString()
    @IsNotEmpty()
    identifier: string;

    @IsString()
    @IsNotEmpty()
    username: string;

    @IsString()
    @IsNotEmpty()
    password: string;
}