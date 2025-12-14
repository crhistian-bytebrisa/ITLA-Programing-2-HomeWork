export interface AnalysisDTO<T> {
    page: number,
    size: number,
    totalCount: number,
    totalPage: number,
    data: T[]
}