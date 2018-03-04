export class ReleasePlan {
    public  releaseName:string;
    public  description: string;
    public  releaseDate: string;
    public startDate: string;
    public status: string;
    public projectId : number;
    public actualReleaseDate?:Date;
    public releasePlanId ?: number   
}