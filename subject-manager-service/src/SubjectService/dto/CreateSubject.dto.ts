import {IsEnum, IsNotEmpty, IsNumber, IsString} from "class-validator";
import {Years} from "../../UserService/entities/student.entity";

export class CreateSubjectDto {

    @IsNotEmpty()
    @IsString()
    name: string;

    @IsNotEmpty()
    @IsString()
    description: string;

    @IsNotEmpty()
    @IsEnum(Years)
    year: Years;

    @IsNotEmpty()
    @IsNumber()
    packet: number;
}