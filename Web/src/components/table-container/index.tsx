import styles from './table-container.module.scss';

interface Props {
  children: React.ReactNode;
}

export default function TableContainer({ children }: Props) {
  return <div className={styles.tableResponsive}>{children}</div>;
}
