import {UUID} from "../../Utils/Types";
import {IsNotEmpty, IsString} from "class-validator";

export class UseIdentifierDto {

    @IsNotEmpty()
    @IsString()
    userId: UUID;

    @IsNotEmpty()
    @IsString()
    value: string;
}