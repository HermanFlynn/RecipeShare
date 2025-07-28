#!/bin/sh

# Remove stale Supervisor socket file if it exists
if [ -e /var/run/supervisor.sock ]; then
    echo "Removing stale supervisor.sock"
    rm -f /var/run/supervisor.sock
fi

# Start supervisord
exec /usr/bin/supervisord -c /etc/supervisor/supervisord.conf