import {UUID} from "../../Utils/Types";
import {IsEnum, IsNotEmpty, IsNumber, IsOptional, IsString} from "class-validator";
import {UserRole} from "../../UserService/entities/user.entity";
import {Years} from "../../UserService/entities/student.entity";

export class CreateIdentifierDto {

    @IsString()
    @IsNotEmpty()
    userId: UUID

    @IsNotEmpty()
    @IsString()
    role: string

    @IsString()
    @IsOptional()
    name: string;

    @IsString()
    @IsOptional()
    credits: string;

    @IsString()
    @IsOptional()
    grade: string;

    @IsString()
    @IsOptional()
    year: string;
}