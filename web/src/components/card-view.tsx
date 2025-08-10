import type { IEntityTableCell } from "../types/entity-table-cell";
import type { IHeadCell } from "../types/head-cells";

interface CardViewProps {
    entities: IEntityTableCell[];
    headCells: IHeadCell[];
    loading?: boolean;
    error: Error | null;
}

export function CardView({ entities, headCells, loading = false, error }: CardViewProps) {
    const skeletonCards = Array.from({ length: 3 });

    return (
        <div className="space-y-4 p-4">
            {loading ? (
                skeletonCards.map((_, idx) => (
                    <div key={idx} className="bg-white border border-gray-200 rounded-xl p-4 shadow-sm">
                        <div className="h-6 w-3/4 bg-gray-200 rounded animate-pulse mb-4"></div>
                        <div className="space-y-3">
                            {headCells.slice(0, 4).map((_, cellIdx) => (
                                <div key={cellIdx} className="flex justify-between">
                                    <div className="h-4 w-1/3 bg-gray-200 rounded animate-pulse"></div>
                                    <div className="h-4 w-1/2 bg-gray-200 rounded animate-pulse"></div>
                                </div>
                            ))}
                        </div>
                    </div>
                ))
            ) : error ? (
                <div className="bg-white border border-gray-200 rounded-xl p-8 shadow-sm text-center">
                    <div className="text-red-500 text-lg mb-2">⚠️</div>
                    <p className="text-gray-600">{error.message}</p>
                </div>
            ) : entities.length === 0 ? (
                <div className="bg-white border border-gray-200 rounded-xl p-8 shadow-sm text-center">
                    <p className="text-gray-500">Nenhum dado encontrado.</p>
                </div>
            ) : (
                entities.map((row) => (
                    <div
                        key={row.id}
                        className="bg-white border border-gray-200 rounded-xl p-4 shadow-sm hover:shadow-md transition-all duration-200 hover:-translate-y-1"
                    >
                        <div className="border-b border-gray-100 pb-3 mb-3">
                            <h3 className="font-semibold text-gray-800 text-lg">
                                {row.elements[0]?.value}
                            </h3>
                        </div>
                        <div className="space-y-3">
                            {row.elements.slice(1).map((cell, cellIdx) => {
                                const headCell = headCells[cellIdx + 1];
                                
                                return headCell ? (
                                    <div
                                        key={cellIdx}
                                        className="flex justify-between items-center py-1"
                                    >
                                        <span className="text-sm font-medium text-gray-600">
                                            {headCell.label}
                                        </span>
                                        <span className="text-sm text-gray-800 text-right max-w-[60%] truncate">
                                            {cell.value}
                                        </span>
                                    </div>
                                ) : null;
                            })}
                        </div>
                    </div>
                ))
            )}
        </div>
    );
}