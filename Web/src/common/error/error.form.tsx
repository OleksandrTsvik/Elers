import ErrorAlert from './error.alert';

import styles from './error.module.scss';

interface Props {
  error: unknown;
}

export default function ErrorForm({ error }: Props) {
  if (!error) {
    return null;
  }

  return <ErrorAlert className={styles.form} error={error} />;
}
