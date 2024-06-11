import { DetailedHTMLProps, HTMLAttributes } from 'react';

import styles from './table-container.module.scss';

interface Props
  extends DetailedHTMLProps<HTMLAttributes<HTMLDivElement>, HTMLDivElement> {}

export default function TableContainer({
  className,
  children,
  ...props
}: Props) {
  return (
    <div className={`${styles.tableResponsive} ${className ?? ''}`} {...props}>
      {children}
    </div>
  );
}
