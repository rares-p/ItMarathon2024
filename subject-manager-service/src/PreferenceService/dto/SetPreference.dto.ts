import {ArrayMaxSize, ArrayMinSize, IsArray, IsNotEmpty, IsString, ValidateNested} from "class-validator";
import {Preference} from "../preference.service";
import {UUID} from "../../Utils/Types";

export class SetPreferenceDto {

    @IsNotEmpty()
    @IsString()
    studentId: UUID;

    @IsArray()
    @ArrayMinSize(4)
    @ArrayMaxSize(4)
    @ValidateNested({each: true})
    preferences: Array<Preference>;
}