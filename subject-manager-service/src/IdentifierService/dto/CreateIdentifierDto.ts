import {UUID} from "../../Utils/Types";
import {IsEnum, IsNotEmpty, IsNumber, IsOptional, IsString} from "class-validator";
import {UserRole} from "../../UserService/entities/user.entity";
import {Years} from "../../UserService/entities/student.entity";

export class CreateIdentifierDto {

    @IsString()
    @IsNotEmpty()
    userId: UUID

    @IsNotEmpty()
    @IsEnum(UserRole)
    role: UserRole

    @IsString()
    @IsOptional()
    name: string;

    @IsNumber()
    @IsOptional()
    credits: number;

    @IsNumber()
    @IsOptional()
    grade: number;

    @IsEnum(Years)
    @IsOptional()
    year: Years;
}