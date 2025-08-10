import { useMobile } from "../hooks/use-mobile";
import type { IEntityTableCell } from "../types/entity-table-cell";
import type { IHeadCell } from "../types/head-cells";
import { CardView } from "./card-view";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "./ui/table";

interface EntityTableProps {
    entities: IEntityTableCell[];
    headCells: IHeadCell[];
    loading?: boolean;
    error: Error | null;
}

export function EntityTable({ entities, headCells, loading = false, error }: EntityTableProps) {
    const isMobile = useMobile(768);
    const skeletonRows = Array.from({ length: 5 });

    if (isMobile) {
        return (
            <div className="w-full h-full">
                <CardView
                    entities={entities}
                    headCells={headCells}
                    loading={loading}
                    error={error}
                />
            </div>
        );
    }

    return (
        <div className="w-full h-full p-4">
            <div className="w-full overflow-x-auto border border-gray-200 rounded-xl shadow-sm bg-white md:rounded-t-xl">
                <Table className="w-full text-sm min-w-[600px]">
                    <TableHeader className="bg-blue-100">
                        <TableRow>
                            {headCells.map((headCell, index) => (
                                <TableHead
                                    key={index}
                                    className="font-semibold text-gray-700 uppercase tracking-wide px-4 py-3 whitespace-nowrap"
                                    style={{
                                        textAlign: headCell.align || "left",
                                        width: headCell.width
                                    }}
                                >
                                    {headCell.label}
                                </TableHead>
                            ))}
                        </TableRow>
                    </TableHeader>

                    <TableBody>
                        {loading ? (
                            skeletonRows.map((_, rowIdx) => (
                                <TableRow key={`skeleton-${rowIdx}`}>
                                    {headCells.map((_, cellIdx) => (
                                        <TableCell key={cellIdx} className="px-4 py-3">
                                            <div className="h-4 w-full bg-gray-200 rounded animate-pulse"></div>
                                        </TableCell>
                                    ))}
                                </TableRow>
                            ))
                        ) : error ? (
                            <TableRow>
                                <TableCell colSpan={headCells.length} className="text-center text-gray-500 px-4 py-8">
                                    <div className="flex flex-col items-center space-y-2">
                                        <div className="text-red-500 text-2xl">⚠️</div>
                                        <p>{error.message}</p>
                                    </div>
                                </TableCell>
                            </TableRow>
                        ) : entities.length === 0 ? (
                            <TableRow>
                                <TableCell colSpan={headCells.length} className="text-center text-gray-500 px-4 py-8">
                                    <div className="flex flex-col items-center space-y-2">
                                        <p>Nenhum dado encontrado.</p>
                                    </div>
                                </TableCell>
                            </TableRow>
                        ) : (
                            entities.map((row) => (
                                <TableRow
                                    key={row.id}
                                    className="hover:bg-gray-50 transition-colors duration-150"
                                >
                                    {row.elements.map((cell, cellIdx) => (
                                        <TableCell
                                            key={cellIdx}
                                            className="px-4 py-3 text-gray-600 whitespace-nowrap"
                                        >
                                            {cell.value}
                                        </TableCell>
                                    ))}
                                </TableRow>
                            ))
                        )}
                    </TableBody>
                </Table>
            </div>
        </div>
    );
}